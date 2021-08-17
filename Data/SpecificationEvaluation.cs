using API.Entities;
using API.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class SpecificationEvaluation<T> where T : BaseEntity
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {

            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);

            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

    }
}
