using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Bazaar.Domain.Dtos;
using Bazaar.Domain.Entities;
using Bazaar.Repository;

namespace Bazaar.Controllers.Api
{
    public class FaqController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FaqController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //GET /api/faqItems
        public IEnumerable<FaqItemDto> GetFaqItems()
        {
            return _unitOfWork.FaqItem.GetFaqItems().Select(Mapper.Map<FaqItem,FaqItemDto>);
        }

        //GET /api/faqItems/1
        public IHttpActionResult GetFaqItem(int id)
        {
            var faqItem = _unitOfWork.FaqItem.GetFaqItem(id);

            if (faqItem == null)
                return NotFound();

            return Ok(Mapper.Map<FaqItem,FaqItemDto>(faqItem));
        }

        //POST /api/FaqItems
        [HttpPost]
        public IHttpActionResult CreateFaqItem(FaqItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var faqItem = Mapper.Map<FaqItemDto, FaqItem>(itemDto);
            _unitOfWork.FaqItem.AddFaqItem(faqItem);
            _unitOfWork.Complete();

            itemDto.FaqItemId = faqItem.FaqItemId;

            return Created(new Uri(Request.RequestUri + "/" + itemDto.FaqItemId), itemDto);
        }

        //PUT /api/FaqItems/
        [HttpPut]
        public void UpdateFaqItem(int id, FaqItemDto item)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var faqItem = Mapper.Map<FaqItem>(item);
            _unitOfWork.FaqItem.UpdateFaqItem(id, faqItem);
            _unitOfWork.Complete();
            
        }

        //DELETE /api/FaqItems/1
        [HttpDelete]
        public void DeleteFaqItem(int id)
        {
            var faqItem = _unitOfWork.FaqItem.GetFaqItem(id);

            if (faqItem == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _unitOfWork.FaqItem.DeleteFaqItem(id);
            _unitOfWork.Complete();

        }

    }
}
