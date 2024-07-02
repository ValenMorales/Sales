using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.Model.Models;

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Role
            CreateMap<Role, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region User
            CreateMap<User, UserDTO>().ForMember(
                destination =>
                destination.RoleDescription,
               opt => opt.MapFrom(origin => origin.Role.Name)
               )
            .ForMember(
                    destination =>
                    destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                    );


            CreateMap<User, SessionDTO>().ForMember(
                destination =>
                destination.RoleDescription,
               opt => opt.MapFrom(origin => origin.Role.Name)
               );



            CreateMap<UserDTO, User>().ForMember(
                destination =>
                destination.Role,
               opt => opt.Ignore()
               )
            .ForMember(destination =>
            destination.IsActive,
            opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false));
            #endregion

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, ProductDTO>().
                ForMember(destination =>
                destination.CategoryDescription,
                opt => opt.MapFrom(origin => origin.Category.Name)
                )
                .ForMember(destination =>
                destination.Price,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<ProductDTO, Product>().
            ForMember(destination =>
            destination.Category,
            opt => opt.Ignore()
            )
            .ForMember(destination =>
            destination.Price,
            opt => opt.MapFrom(origin => Convert.ToString(origin.Price, new CultureInfo("es-CO")))
            )
            .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );

            #endregion

            #region Sale
            CreateMap<Sale, SaleDTO>().
                ForMember(destination =>
                destination.TextTotal,
                opt => opt.MapFrom(origin =>
                Convert.ToString(origin.Total.Value, new CultureInfo("es-Co"))
                )
                ).ForMember(destination =>
                destination.RegisterDate,
                opt => opt.MapFrom(origin => origin.RegisterDate.Value.ToString("dd/MM/yyyy")
                ));

            CreateMap<SaleDTO, Sale>().
                ForMember(destination =>
                destination.Total,
                opt => opt.MapFrom(origin =>
                Convert.ToDecimal(origin.TextTotal, new CultureInfo("es-CO"))
                ));

            #endregion


            #region SaleDetail
            CreateMap<SaleDetail, SaleDetailDTO>().
                ForMember(destination =>
                destination.ProductDescription,
                opt => opt.MapFrom(origin =>
               origin.Product.Name
                ))
                .ForMember(destination =>
                destination.ClientName,
                opt => opt.MapFrom(origin =>
               origin.Client.Name
                ))
                .ForMember(destination =>
                destination.TextPrice,
                opt => opt.MapFrom(
                    origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO"))
                ))
                 .ForMember(destination =>
                destination.TextTotal,
                opt => opt.MapFrom(
                    origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO"))
                ));

            CreateMap<SaleDetailDTO, SaleDetail>().
                ForMember(destination =>
                destination.Price,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TextPrice, new CultureInfo("es-CO"))
                ))
            .ForMember(destination =>
             destination.Total,
             opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TextTotal, new CultureInfo("es-CO"))
             ));
            #endregion

            #region Reporte
            CreateMap<SaleDetail, ReportDTO>().
                ForMember(destination =>
                destination.RegisterDate,
                opt => opt.MapFrom(origin => origin.Sale.RegisterDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destination =>
                destination.DocumentNumber,
                opt => opt.MapFrom(origin => origin.Sale.DocumentNumber)
                )
                .ForMember(destination =>
                destination.PayMethod,
                opt => opt.MapFrom(origin => origin.Sale.PayMethod)
                )
                .ForMember(destination =>
                destination.TotalSale,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Sale.Total.Value, new CultureInfo("es-CO")
                )))
                .ForMember(destination =>
                destination.Product,
                opt => opt.MapFrom(origin => origin.Product.Name)
                )
                .ForMember(destination =>
                destination.Price,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO"))
                ))
                .ForMember(destination =>
                destination.Total,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO"))
                ));

            #endregion
        }

    }
}
