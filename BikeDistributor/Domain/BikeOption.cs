using BikeDistributor.Infrastructure.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Domain
{
    //this class may also be used to create totally new options besides Color, Material, Gear
    //the only requirement is every option should have a Name != "" and must be unique for a given BikeVariant
    //the latter requirement is taken care by the SetTotalPrice method in the variant class which is the only possible way to add an option
    //TODO: [well no] add a shortcut constructor like NewOption(string name, string description, int price) that throw and Exception for names Color, Material, Gear
    public class BikeOption : IBikeOption
    {
        public virtual string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        //TODO: Option New Constructor should be made private at somepoint
        //Create(string name) override should be made avalaible only for Option base class and not for Color, Material etc..
        public BikeOption Create(string description, int price)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw new Exception("cannot add an option with no name");
            }
            this.Description = description;
            this.Price = price;

            return this;
        }

        public static BikeOption Create(string name)
        {
            string ns = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            var myClassType = Type.GetType(String.Format("{0}.{1}", @ns, @name));
            IBikeOption instance = myClassType == null ? null : (IBikeOption)Activator.CreateInstance(myClassType);
            //if (instance!=null)
            //{
            //    //throw new Exception("Please use class " + instance.GetType() + " for this kind of option"); 
            //}
            //else
            if (instance == null)
            {
                instance = new BikeOption();
                instance.Name = name;
            }

            return (BikeOption)instance;
        }
    }
    //those classes below may add properties (but it is not that safe unless I make words Color, Material, Gear reserved
    public sealed class Color : BikeOption
    {
        public override string Name => "Color";
    }
    public sealed class Material : BikeOption
    {
        public override string Name => "Material";
    }
    public sealed class Gear : BikeOption
    {
        public override string Name => "Gear";
    }
}
