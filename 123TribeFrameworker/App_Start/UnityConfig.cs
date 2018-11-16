using System;
using System.Data.Entity;
using _123TribeFrameworker.Controllers;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.DAO.BussinessDAO;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Models;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace _123TribeFrameworker
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<IOutRecordService, OutRecordServiceImpl>();
            container.RegisterType<IFirstLevelDirService, FirstLevelDirServiceImpl>();
            
            container.RegisterType<IRoleMenuLayerService, RoleMenuLayerImpl>();
            container.RegisterType<IDirLayerService, DirLayerServiceImpl>();

            container.RegisterType<IMaterialInfoDAO, MaterialInfoDAO>();
            container.RegisterType<IMaterialInfoService, MaterialInfoServiceImpl>();

            container.RegisterType<IOrderInfoService, OrderInfoServiceImpl>();
            container.RegisterType<IOrderInfoDAO, OrderInfoDAO>();

            container.RegisterType<IOrderDetailInfoService, OrderDetailInfoServiceImpl>();
            container.RegisterType<IOrderDetailInfoDAO, OrderDetailInfoDAO>();

            container.RegisterType<IInventoryService, InventoryServiceImpl>();
            container.RegisterType<IInventoryDAO, InventoryDAO>();

            container.RegisterType<IInStorageRecordService, InStorageRecordServiceImpl>();
            container.RegisterType<IInStorageRecordDAO, InStorageRecordDAO>();

            container.RegisterType<ISaleDAO, SaleDAO>();
            container.RegisterType<ISaleService, SaleServiceImpl>();

            container.RegisterType<IOutRecordDAO, OutRecordDAO>();
            container.RegisterType<IOutRecordService, OutRecordServiceImpl>();

            container.RegisterType<ISecondLevelDirDAO, SecondLevelDirDAO>();
            container.RegisterType<ISecondLevelDirService, SecondLevelDirServiceImpl>();
        }
    }
}