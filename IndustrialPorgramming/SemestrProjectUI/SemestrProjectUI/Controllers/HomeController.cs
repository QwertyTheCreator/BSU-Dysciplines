using Microsoft.AspNetCore.Mvc;
using SemesterProjectUI.Models.EquationDirector;
using SemesterProjectUI.Models.Equations;
using SemesterProjectUI.Models.Responses;
using SemesterProjectUI.Services.ExpressionsServices;
using SemesterProjectUI.Services.OutputServices;

namespace SemesterProjectUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEquationService _equationService;
        private readonly IOutputService _outputService;

        public HomeController(ILogger<HomeController> logger, IEquationService equationService, IOutputService outputService)
        {
            _logger = logger;
            _equationService = equationService;
            _outputService = outputService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("StarterView");
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
            VariableResponse variableResponse = new VariableResponse(equations);

            //DataBase.DataBase.UserForm = inputForm;
            //DataBase.DataBase.variableResponse = variableResponse;

            if (equations.GetVariablesCount() != 0)
            {
                //return VariableInput(variableResponse);
                return View("VariableInput", variableResponse);
            }

            return View("Output", variableResponse);//TODO Write View for output
        }

        [HttpGet]
        public IActionResult VariableInput()
        {
            return View("VariableInput");
        }

        [HttpPut]
        public IActionResult VariableInput(VariableResponse variableResponse)
        {
            //DataBase.DataBase.variableResponse = variableResponse;
            return View("Output", variableResponse);
        }

        [HttpGet]
        public IActionResult Output(VariableResponse variableResponse)
        {
            variableResponse.CreateAnswer();
            _outputService.CreateOutput(variableResponse.equations!, DataBase.DataBase.UserForm!);

            return View();
        }
    }
}
