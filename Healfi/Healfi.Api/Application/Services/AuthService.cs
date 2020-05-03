using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Healfi.Api.Application.Commands;
using Healfi.Api.Application.Common;
using Healfi.Api.Data;
using Healfi.Api.Data.Extensions;
using Healfi.Api.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Healfi.Api.Application.Services
{
    public class AuthService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly HealfiContext _context;

        public AuthService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration, HealfiContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<(bool success, string message, AuthenticationResult result)> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

            if (!result.Succeeded)
            {
                return (false, "Usuário ou senha inválidos", null);
            }

            return await GenerateToken(email);
        }

        public async Task<(bool success, string message, AuthenticationResult result)> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));
            identityClaims.AddClaim(new Claim("Healfi.Claims.Id", user.Id.ToString()));
            identityClaims.AddClaim(new Claim("Healfi.Claims.Name", user.Nome));
            identityClaims.AddClaim(new Claim("Healfi.Claims.Type", user.Tipo.ToString()));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Security:Secret"]);
            var createdAt = DateTime.Now;
            var valid = createdAt.AddHours(6);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = valid,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenResult = new AuthenticationResult()
            {
                AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                CreatedAt = createdAt,
                ExpiresAt = valid
            };
            
            return (true, null, tokenResult);
        }

        public async Task<(bool success, string message, AuthenticationResult result)> Register(CreateProdutorCommand source)
        {
            var produtorId = Guid.NewGuid();
            var usuario = new Usuario()
            {
                Email = source.Email,
                Tipo = source.TipoVinculo,
                Nome = source.Nome,
                PhoneNumber = source.Telefone,
                UserName = source.Email ?? source.Telefone,
                Produtor = new Produtor
                {
                    Id = produtorId,
                    CadastroCompleto = false
                }
            };
            var r = await RegisterUser(usuario, source.Senha);
            
            if (r.success)
            {
                var end = Guid.NewGuid();
                _context.EnderecosProdutor.Adicionar(new EnderecoProdutor()
                {
                    Id = end,
                    ProdutorId = produtorId,
                    Endereco = new Endereco()
                    {
                        Id = Guid.NewGuid(),
                        CidadeId = source.CidadeId
                    }
                });

                usuario.Produtor.EnderecoId = end;

                _context.Produtores.Update(usuario.Produtor);
            }

            await _context.SaveChangesAsync();
            
            return r;
        }
        
        public Task<(bool success, string message, AuthenticationResult result)> Register(CreateConsumidorCommand source)
        {
            return RegisterUser(new Usuario()
            {
                Email = source.Email,
                Tipo = source.TipoVinculo,
                Nome = source.Nome,
                PhoneNumber = source.Telefone,
                UserName = source.Email ?? source.Telefone,
                Consumidor = new Consumidor
                {
                    Id = Guid.NewGuid(),
                    CidadePadraoId = source.CidadePadraoId
                }
            }, source.Senha);
        }

        private async Task<(bool success, string message, AuthenticationResult result)> RegisterUser(Usuario user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);

            if (createResult.Succeeded)
            {
                return await GenerateToken(user.Email);
            }

            return (false, "Não foi possível criar a conta", null);
        }
    }
}