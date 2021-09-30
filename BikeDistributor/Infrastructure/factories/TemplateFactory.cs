using BikeDistributor.Domain;
using MV.Framework.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeDistributor.Domain.Models;

namespace BikeDistributor.Infrastructure.factories
{
    /*
      * example of a catch all factory https://stackoverflow.com/questions/40915110/reflection-call-constructor-with-parameters
      * whether it is better to have a catch all factory or many specialized factories has to be evaluated
      */
    public class TemplateFactory
    {
        public static ITemplateRenderer Create(Order order, string recieptFormat, string mynamespace = "BikeDistributor.Infrastructure.core")
        {

            string myClassName = recieptFormat + "Renderer";
            var myClassType = Type.GetType(String.Format("{0}.{1}", new object[] { mynamespace, myClassName }));
            var parametrizedCtor = myClassType
            .GetConstructors()
            .FirstOrDefault(c => c.GetParameters().Length > 0);
            try
            {
                var instance = parametrizedCtor.Invoke(new object[] { order });
                return (ITemplateRenderer)instance;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " NOT FOUND CLASS IS OF TYPE " + myClassType);
            }

            //ITemplateRenderer instance = (ITemplateRenderer)(myClassType == null ? null : (ITemplateRenderer)Activator.CreateInstance(myClassType));

        }
    }
}
