using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.WebUI.GraphQL;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    [Route("graphql")]
    [ApiController]
    //[Authorize]
    public class GraphQLController : ApiController
    {
    public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
    {
      var inputs = query.Variables.ToInputs();

      var schema = new Schema
      {
        Query = new AuthorQuery()
      };

      var result = await new DocumentExecuter().ExecuteAsync(_ =>
      {
        _.Schema = schema;
        _.Query = query.Query;
        _.OperationName = query.OperationName;
        _.Inputs = inputs;
      });

      if (result.Errors?.Count > 0)
      {
        return BadRequest();
      }

      return Ok(result);
    }

  }
}