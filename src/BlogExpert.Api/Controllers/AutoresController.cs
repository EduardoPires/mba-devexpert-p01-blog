﻿using AutoMapper;
using BlogExpert.Api.Models;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogExpert.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IAutorService _autorService;
        private readonly IAutorRepository _autorRepository;
        public AutoresController(IMapper mapper, IAutorService autorService, IAutorRepository autorRepository, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _autorService = autorService;
            _autorRepository = autorRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<AutorModel>>> ObterTodos()
        {
            var autoresModel = _mapper.Map<IEnumerable<AutorModel>>(await _autorRepository.Listar());

            if (autoresModel == null) return NotFound();

            return autoresModel.ToList();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AutorModel>> ObterPorId(Guid id)
        {
            var autorModel = await ObterAutorModel(id);

            if (autorModel == null) return NotFound();

            return autorModel;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AutorModel>> Adicionar(AutorModel autorModel)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _autorService.Adicionar(_mapper.Map<Autor>(autorModel));

            return CustomResponse(HttpStatusCode.Created, await ObterAutorModel(autorModel.Id));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AutorModel>> Atualizar(Guid id, AutorModel autorModel)
        {
            if (id != autorModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _autorService.Atualizar(_mapper.Map<Autor>(autorModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AutorModel>> Excluir(Guid id)
        {
            await _autorService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<AutorModel> ObterAutorModel(Guid id)
        {
            return _mapper.Map<AutorModel>(await _autorRepository.ObterPorId(id));
        }
    }
}
