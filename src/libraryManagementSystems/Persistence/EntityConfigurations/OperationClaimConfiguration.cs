using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Reservations.Constants;
using Application.Features.Orders.Constants;
using Application.Features.CargoInformations.Constants;
using Application.Features.CargoTrackings.Constants;
using Application.Features.CreditCarts.Constants;
using Application.Features.Managements.Constants;
using Application.Features.Employees.Constants;
using Application.Features.InventoryManagements.Constants;
using Application.Features.Books.Constants;
using Application.Features.Shelves.Constants;
using Application.Features.Categories.Constants;



namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
     
        

        #region Reservations
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Read },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Write },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Create },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Update },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Orders
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OrdersOperationClaims.Admin },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Read },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Write },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Create },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Update },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CargoInformations
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Read },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Write },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Create },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Update },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CargoTrackings
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Read },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Write },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Create },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Update },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CreditCarts
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Read },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Write },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Create },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Update },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Orders
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OrdersOperationClaims.Admin },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Read },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Write },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Create },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Update },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Managements
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Read },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Write },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Create },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Update },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Employees
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Admin },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Read },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Write },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Create },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Update },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region InventoryManagements
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Admin },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Read },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Write },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Create },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Update },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CargoInformations
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Read },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Write },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Create },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Update },
                new() { Id = ++lastId, Name = CargoInformationsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CargoTrackings
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Read },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Write },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Create },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Update },
                new() { Id = ++lastId, Name = CargoTrackingsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CreditCarts
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Admin },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Read },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Write },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Create },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Update },
                new() { Id = ++lastId, Name = CreditCartsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Employees
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Admin },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Read },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Write },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Create },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Update },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region InventoryManagements
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Admin },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Read },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Write },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Create },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Update },
                new() { Id = ++lastId, Name = InventoryManagementsOperationClaims.Delete },
            ]
        );
        #endregion
        
        

        
        
        #region Managements
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Read },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Write },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Create },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Update },
                new() { Id = ++lastId, Name = ManagementsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Orders
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OrdersOperationClaims.Admin },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Read },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Write },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Create },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Update },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Employees
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Admin },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Read },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Write },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Create },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Update },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Delete },
            ]
        );
        #endregion
                
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
                
        
        #region Shelves
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ShelvesOperationClaims.Admin },
                new() { Id = ++lastId, Name = ShelvesOperationClaims.Read },
                new() { Id = ++lastId, Name = ShelvesOperationClaims.Write },
                new() { Id = ++lastId, Name = ShelvesOperationClaims.Create },
                new() { Id = ++lastId, Name = ShelvesOperationClaims.Update },
                new() { Id = ++lastId, Name = ShelvesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Categories
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
