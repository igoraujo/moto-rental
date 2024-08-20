using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoRental.Borders.Constants;
using MotoRental.Borders.Models;
using MotoRental.UseCases.Motocycles;

namespace MotoRental.Admin.Controllers;

public class MotorcycleController : Controller
{
    private readonly IMotorcycleUseCase _useCase;
    private readonly ILogger _logger;
    // private readonly ILogUtil _logUtil;
    private RouteData _context;

    public MotorcycleController(IMotorcycleUseCase useCase, ILogger<MotorcycleController> logger)
    {
        _useCase = useCase;
        _logger = logger;
    }

    public async Task<ActionResult> Index()
    {
        IEnumerable<Motorcycle>? response = new List<Motorcycle>();

        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            response = await _useCase.FindAll();
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response);
    }

    // [Authorize(Roles = Roles.ADMIN)]
    public ActionResult Create()
    {
        return View(new Motorcycle());
    }

    [HttpPost]
    [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> CreateDevice(Motorcycle request)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            await _useCase.Save(request);

            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");

        }

        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);

    }

    [HttpDelete]
    // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> Delete(int id)
    {
        _context = ControllerContext.RouteData;

        try
        {
            var response = await _useCase.Delete(new Motorcycle{Id = id});
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");
        }
    }

    // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> Update(int id)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        Motorcycle response = null;

        try
        {
            response = await _useCase.FindById(id);
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response);

    }

    [HttpPost]
    // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> UpdateDevice(Motorcycle request)
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            var response = await _useCase.Update(request);

            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");
        }

        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
    }

    [HttpPost]
    public ActionResult Cancel()
    {
        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
    }

    private ActionResult Forbidden()
    {
        _context = this.ControllerContext.RouteData;
        // _logger.LogWarning(_logUtil.Forbidden(GetType().FullName,
        // _context.Values[ActionName.ACTION].ToString()));
        return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
    }
}