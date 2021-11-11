- turn off auto validation
  `services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });`

- add response for all error request
  c#`services.AddMvc().ConfigureApiBehaviorOptions(options => { options.InvalidModelStateResponseFactory = c => { var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0) .SelectMany(v => v.Errors) .Select(v => v.ErrorMessage)); return new BadRequestObjectResult(new { status = false, Message = errors }); }; });`
