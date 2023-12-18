using Microsoft.AspNetCore.Mvc;
using SemesterProjectUI.Models.Responses;
using SemesterProjectUI.Services.ExpressionsServices;
using SemesterProjectUI.Services.OutputServices;

namespace SemesterProjectUI.Controllers
{
    public class OutputController : Controller
    {
        private readonly ILogger<OutputController> _logger;
        private readonly IEquationService _equationService;
        private readonly IOutputService _outputService;

        public OutputController(ILogger<OutputController> logger, IEquationService equationService, IOutputService outputService)
        {
            _logger = logger;
            _equationService = equationService;
            _outputService = outputService;
        }

        [HttpGet]
        public IActionResult Output(VariableResponse variableResponse)
        {
            DataBase.DataBase.variableResponse!.CreateAnswer();
            _outputService.CreateOutput(DataBase.DataBase.variableResponse.equations!, DataBase.DataBase.UserForm!);

            return View();
        }
    }
}
