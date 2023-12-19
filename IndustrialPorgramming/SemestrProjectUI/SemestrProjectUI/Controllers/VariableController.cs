using Microsoft.AspNetCore.Mvc;
using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Responses;
using SemesterProjectUI.Services.ExpressionsServices;
using SemesterProjectUI.Services.OutputServices;

namespace SemesterProjectUI.Controllers
{
    public class VariableController : Controller
    {
        private readonly ILogger<VariableController> _logger;
        private readonly IEquationService _equationService;
        private readonly IOutputService _outputService;

        public VariableController(ILogger<VariableController> logger, IEquationService equationService, IOutputService outputService)
        {
            _logger = logger;
            _equationService = equationService;
            _outputService = outputService;
        }

        [HttpGet]
        public IActionResult VariableInput()
        {
            return View("VariableInput", DataBase.DataBase.variableResponse);
        }

        [HttpPost]
        public IActionResult VariableInput(VariableResponse variableResponse)
        {
            //DataBase.DataBase.variableResponse = variableResponse;
            DataBase.DataBase.variableResponse!.variablesValues = variableResponse.variablesValues;
            return RedirectToAction("Output", "Output", DataBase.DataBase.variableResponse);
        }
    }
}
