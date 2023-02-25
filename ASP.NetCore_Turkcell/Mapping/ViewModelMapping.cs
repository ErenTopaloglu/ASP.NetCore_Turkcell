using ASP.NetCore_Turkcell.Models;
using ASP.NetCore_Turkcell.ViewModel;
using AutoMapper;

namespace ASP.NetCore_Turkcell.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
