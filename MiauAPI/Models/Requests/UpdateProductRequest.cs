using MiauDatabase.Enums;

namespace MiauAPI.Models.Requests;

/// <summary>
/// Represents a request for update a product by a given Id.
/// </summary>
/// <param name="Id">The id of the product.</param>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Price">The price of the product.</param>
/// <param name="IsActive">Determines whether this product is available for sale.</param>
/// <param name="Amount">The product's amount in stock.</param>
/// <param name="Tags">The product's store tags.</param>
/// <param name="Brand">The product's brand.</param>
/// <param name="Discount">The password entered by the user.</param>
public sealed record UpdateProductRequest(int Id, string Name, string Description, decimal Price, bool IsActive, int Amount, ProductTag Tags, string? Brand, decimal Discount);

