using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoRental.Admin.Models;
using MotoRental.Borders.Constants;
using MotoRental.Borders.Models;
using MotoRental.UseCases.Products;
using MotoRental.UseCases.Rentals;
using System.Reflection;

namespace MotoRental.Admin.Controllers;

public class RentalController : Controller
{
    private readonly IRentalUseCase _rentalUseCase;
    private readonly IProductUseCase _productUseCase;
    // private readonly IDeliveryPesonUseCase _deliveryPesonUseCase;

    private readonly ILogger _logger;

    // private readonly ILogUtil _logUtil;
    private RouteData _context;

    public RentalController(IRentalUseCase rentalUseCase, ILogger<RentalController> logger, IProductUseCase productUseCase)
    {
        _rentalUseCase = rentalUseCase;
        _productUseCase = productUseCase;
        _logger = logger;
    }

    public async Task<ActionResult> Index()
    {
        IEnumerable<Rental>? response = new List<Rental>();

        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            response = await _rentalUseCase.FindAll();
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response);
    }

    public async Task<ActionResult> Index(int deliveryPersonId)
    {
        IEnumerable<Rental>? response = new List<Rental>();

        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            response = await _rentalUseCase.FindByDeliveryPersonId(deliveryPersonId);
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response);
    }

    // [Authorize(Roles = Roles.ADMIN)]
    [Route("Rental/Create/{productId}")]
    public async Task<ActionResult> Create(int productId)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];
        //cache
        var deliveryPerson = new DeliveryPerson
        {
            Id = 1,
            LicenseType = LicenseType.A
        };
        var products = await _productUseCase.FindAll();
        var product = products?.First(p => p.Id == productId);

        var view = new RentalViewModel
        {
            Products = products,
            Product = product,
            ExpectedEndDate = DateTime.Now.AddDays(product.NumberOfDays),
            Total = product.PricePerDay * product.NumberOfDays
        };
        
        return View(view);
    }

    [HttpPost]
    // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> CreateRental(RentalViewModel request)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            var deliveryPerson = new DeliveryPerson
            {
                Id = 1,
                LicenseType = LicenseType.A
            };
            await _rentalUseCase.Save(request.Product, deliveryPerson);

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
            var response = await _rentalUseCase.Delete(new Rental { Id = id });
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

        Rental response = null;

        try
        {
            response = await _rentalUseCase.FindById(id);
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
    public async Task<ActionResult> UpdateRequest(Rental request)
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            var response = await _rentalUseCase.Update(request);

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