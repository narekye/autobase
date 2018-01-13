using AutoBase.DAL.AutoBaseEntities;
using System.Linq;

namespace AutoBase.DAL.Filters
{
    public class FilterDump
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int? ModuleId { get; set; }
        public int? Year { get; set; }

        public string BlockNumber { get; set; }
        public string Memory { get; set; }

        public IQueryable<Dump> FilterObjects(IQueryable<Dump> query)
        {
            if (MakeId.HasValue)
                query = query.Where(x => x.MakeId == MakeId);
            if (ModelId.HasValue)
                query = query.Where(x => x.ModelId == ModelId);
            if (ModuleId.HasValue)
                query = query.Where(x => x.ModuleId == ModuleId);
            if (Year.HasValue)
                query = query.Where(x => x.Year == Year);
            if (!string.IsNullOrEmpty(BlockNumber))
                query = query.Where(x => x.BlockNumber == BlockNumber);
            if (!string.IsNullOrEmpty(Memory))
                query = query.Where(x => x.Memory == Memory);
            return query;
        }
    }
}
