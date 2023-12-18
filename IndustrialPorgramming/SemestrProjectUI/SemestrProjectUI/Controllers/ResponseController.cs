using Microsoft.AspNetCore.Mvc;
using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Responses;
using SemesterProjectUI.Services.ExpressionsServices;
using SemesterProjectUI.Services.OutputServices;

namespace SemesterProjectUI.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ILogger<ResponseController> _logger;
        private readonly IEquationService _equationService;
        private readonly IOutputService _outputService;

        public ResponseController(ILogger<ResponseController> logger, IEquationService equationService, IOutputService outputService)
        {
            _logger = logger;
            _equationService = equationService;
            _outputService = outputService;
        }

        [HttpGet]
        public IActionResult ResponseForm()
        {
            return View("ResponseForm");
        }

        [HttpPost]
        public IActionResult ResponseForm(InputForm inputForm)
        {
            if (!inputForm.IsValid())
            {
                return View("ResponseForm");
            }

            EquationsDirector equations = _equationService.GetExpressionsFromFile(inputForm!.StarterPath!);
            DataBase.DataBase.variableResponse = new VariableResponse(equations);
            DataBase.DataBase.UserForm = inputForm;
            //DataBase.DataBase.variableResponse = variableResponse;

            if (equations.GetVariablesCount() != 0)
            {
                //return VariableInput(variableResponse);
                //return View("VariableInput", DataBase.DataBase.variableResponse);
                return RedirectToAction("variableInput", "variable" );
            }

            return RedirectToAction("Output", "Output");//TODO Write View for output
        }
    }
}
