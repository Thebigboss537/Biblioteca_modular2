﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuarios.Data;
using Usuarios.Models;
using Usuarios.Repositorio;
using Usuarios.Models.Dto;
using Usuarios.Util;

namespace Usuarios.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        protected ResponseDto _response;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            try
            {
                var lista = await _usuarioRepositorio.GetUsuarios();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de usuarios";
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioRepositorio.GetUsuarioById(id);
            if (usuario == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario no existe";
                return NotFound(_response);
            }
            _response.Result = usuario;
            _response.DisplayMessage = "Informacion del usuario";
            return Ok(_response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDto usuarioDto)
        {
            try
            {

                UsuarioDto model = await _usuarioRepositorio.CreateUpdate(usuarioDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el usuario";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                bool estaeliminado = await _usuarioRepositorio.DeleteUsuario(id);
                if (estaeliminado)
                {
                    _response.Result = estaeliminado;
                    _response.DisplayMessage = "usuario eliminado con exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar el usuario";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpGet("crearusuario")]
        public async Task<ActionResult<IEnumerable<Usuario>>> Dropdownlist()
        {
            try
            {
                gets dropdowns = new()
                {
                    Programas_academicos = await _usuarioRepositorio.GetProgramas_academicos()
            };
                _response.Result = dropdowns;
                _response.DisplayMessage = "drop down list";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPost("crearusuario")]
        public async Task<IActionResult> Crearusuario(UsuarioDto usuario)
        {
            try
            {
                UsuarioDto model = await _usuarioRepositorio.CreateUpdate(usuario);
                _response.Result = model;
                return CreatedAtAction("GetUsuario", new { id = model.Id_usuario }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el usuario";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        /*[HttpPost("crearusuario")]
        public async Task<IActionResult> crearusuario(UsuarioDto usuario)
        {
            var respuesta = await _usuarioRepositorio.Register(
                new Usuario
                {
                    Cedula = usuario.Cedula,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Id_programa_academico = usuario.Id_programa_academico,
                    Telefono = usuario.Telefono,
                    Semestre = usuario.Semestre,
                    Correo_electronico = usuario.Correo_electronico,
                    Estado = Util.Estado.Inactivo
                }, usuario.Contraseña);
            if (respuesta == "existe")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Login_usuario ya existe";
                return BadRequest(_response);
            }

            if (respuesta == "error")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el usuario";
                return BadRequest(_response);
            }

            JwTPackage jpt = new JwTPackage();
            jpt.UserName = usuario.Cedula.ToString();
            jpt.Token = respuesta;
            _response.Result = jpt;
            _response.DisplayMessage = "Usuario creado con exito";
            //_response.Result = respuesta;
            

            return Ok(_response);
        }

        [HttpGet("crearusuario")]
        public async Task<ActionResult<Usuario>> GetProgramas_academicos()
        {
            try
            {
                gets get = new gets();
                get.programa = await _usuarioRepositorio.GetProgramas_academicos();
                get.rol = await _usuarioRepositorio.GetRoles();
                get.estado = (Estado[])Enum.GetValues(typeof(Estado));

                var lista = await _usuarioRepositorio.GetProgramas_academicos();

                _response.Result = get;
                _response.DisplayMessage = "Lista de programas academicos";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }*/

        public class JwTPackage
        {
            public string UserName { get; set; }
            public string Token { get; set; }
        }

        public class gets
        {

            public List<Programa_academicoDto> Programas_academicos { get; set; }
        }
    }
}
