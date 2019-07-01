using AutoMapper;
using DateRangeAccounting.API.Models;
using DateRangeAccounting.DAL.Domain;
using DateRangeAccounting.DAL.Interfaces.Repository;
using DateRangeAccounting.DAL.Interfaces.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DateRangeAccounting.API.Controllers
{
    [RoutePrefix("api/date")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DateRangeController : ApiController
    {
        private readonly IDateRangeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DateRangeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.Ranges;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("find"), HttpPost]
        public async Task<IHttpActionResult> GetByRange([FromBody]DateRangeViewModel rangeModel)
        {
            var ranges = await _repository.GetByRange(_mapper.Map<DateRange>(rangeModel));

            return Ok(_mapper.Map<IEnumerable<DateRangeViewModel>>(ranges));
        }

        [Route(""), HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]DateRangeViewModel rangeModel)
        {
            if (rangeModel == null) return BadRequest();

            _repository.Add(_mapper.Map<DateRange>(rangeModel));
            await _unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
