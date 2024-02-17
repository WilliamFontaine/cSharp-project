using System.ComponentModel.DataAnnotations;
using Shared.ApiModels;
using FluentAssertions;
using Shared;

namespace Test;

[TestClass]
public class VehicleTest
{
    [TestMethod]
    public void LicensePlate_MinLength_Valid()
    {
        var vehicle = new Vehicle
            { LicensePlate = "1234567", Year = 2020, Mileage = 0, VehicleModelId = 1, Energy = VehicleEnergy.Petrol };

        var result = ValidateModel(vehicle);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void LicensePlate_MinLength_Invalid()
    {
        var vehicle = new Vehicle
            { LicensePlate = "123456", Year = 2020, Mileage = 0, VehicleModelId = 1, Energy = VehicleEnergy.Petrol };

        var result = ValidateModel(vehicle);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should()
            .Contain("The field LicensePlate must be a string or array type with a minimum length of '7'.");
    }

    [TestMethod]
    public void LicensePlate_MaxLength_Valid()
    {
        var vehicle = new Vehicle
            { LicensePlate = "123456789", Year = 2020, Mileage = 0, VehicleModelId = 1, Energy = VehicleEnergy.Petrol };

        var result = ValidateModel(vehicle);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void LicensePlate_MaxLength_Invalid()
    {
        var vehicle = new Vehicle
        {
            LicensePlate = "1234567890", Year = 2020, Mileage = 0, VehicleModelId = 1, Energy = VehicleEnergy.Petrol
        };

        var result = ValidateModel(vehicle);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should()
            .Contain("The field LicensePlate must be a string or array type with a maximum length of '9'.");
    }

    [TestMethod]
    public void Year_Range_Valid()
    {
        var vehicle = new Vehicle
            { LicensePlate = "1234567", Year = 2020, Mileage = 0, VehicleModelId = 1, Energy = VehicleEnergy.Petrol };

        var result = ValidateModel(vehicle);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Year_Range_Invalid()
    {
        var vehicle = new Vehicle
            { LicensePlate = "1234567", Year = 1899, Mileage = 0, VehicleModelId = 1, Energy = VehicleEnergy.Petrol };

        var result = ValidateModel(vehicle);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should().Contain("The field Year must be between 1900 and 2100.");
    }

    [TestMethod]
    public void Mileage_Range_Valid()
    {
        var vehicle = new Vehicle
            { LicensePlate = "1234567", Year = 2020, Mileage = 0, VehicleModelId = 1, Energy = VehicleEnergy.Petrol };

        var result = ValidateModel(vehicle);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Mileage_Range_Invalid()
    {
        var vehicle = new Vehicle
            { LicensePlate = "1234567", Year = 2020, Mileage = -1, VehicleModelId = 1, Energy = VehicleEnergy.Petrol };

        var result = ValidateModel(vehicle);

        result.Should().NotBeEmpty();
        result[0].ErrorMessage.Should().Contain("The field Mileage must be between 0 and 2147483647.");
    }


    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }
}