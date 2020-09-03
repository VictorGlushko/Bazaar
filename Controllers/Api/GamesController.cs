using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Bazaar.Domain.Entities;
using Bazaar.Dtos;
using Bazaar.Repository;

namespace Bazaar.Controllers.Api
{
    public class GamesController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public GamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET /api/games
        public IEnumerable<GameDto> GetGames()
        {
            var rs = _unitOfWork.Games.GetGames().ToList().Select(Mapper.Map<Game, GameDto>);

            return rs;
        }


        // GET /api/games/1
        public GameDto GetGame(int id)
        {
            var game = _unitOfWork.Games.GetGame(id);

            if(game == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Game,GameDto>(game);
        }

        // POST /api/game
        [HttpPost]
        public GameDto CreateGame(GameDto gameDto)
        {
            if(ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var game = Mapper.Map<GameDto, Game>(gameDto);

            _unitOfWork.Games.Add(game);
            _unitOfWork.Complete();
            return gameDto;

        }
        
        //PUT /api/game/1
        [HttpPut]
        public void UpdateGame(int id, Game game)
        {

            if (ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var gameInDb = _unitOfWork.Games.GetGame(id);

            if(gameInDb == null)
                throw  new HttpResponseException(HttpStatusCode.NotFound);

            //Aghtung Not full prop (useAutoMapper)
            gameInDb.Name = game.Name;
            gameInDb.Description = game.Description;
            gameInDb.OriginalPrice = game.OriginalPrice;
            gameInDb.FinalPrice = game.FinalPrice;
            gameInDb.Modes = game.Modes;
            gameInDb.DateAdded = game.DateAdded;
            gameInDb.Platforms = game.Platforms;
            gameInDb.Developer = game.Developer;
            
            _unitOfWork.Complete();

        }

        // DELETE /api/games/1
        [HttpDelete]
        public void DeleteGame(int id)
        {
            var game = _unitOfWork.Games.GetGame(id);

            if (game == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _unitOfWork.Games.Remove(game);
            _unitOfWork.Complete();

        }


    }
}
