using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
namespace FactoryAndAbstractFactory
{
    class CustomerFactory
    {
        static ICustomer x = null;
        static void Main1(string[] args)
        {
           
            string type = "NT";
            x = SimpleFactory.CreateRetailCustomer(type); 
            x.TotalBill();
        }
       
    }
    public class Order
    {
        ICustomer x = null;
        
        public Order()
        {

            //x = SimpleFactory.CreateCustomer("N");
        }
     
    }
    public class Inventory
    {
        ICustomer x = null;
        
        public Inventory()
        {
            //x = SimpleFactory.CreateCustomer("D");
        }
       
    }
    public class Invoice
    {
        ICustomer x = null;
        public Invoice()
        {
            x = SimpleFactory.CreateRetailCustomer("D");
        }
    }

    public static class SimpleFactory
    {
        // centralizing new keyword
        static UnityContainer retailCustomerFactory = new UnityContainer();
        static UnityContainer CorporateCustomerFactory = new UnityContainer();

        static SimpleFactory()
        {
            // No discount , Courier , Local taxes
            retailCustomerFactory.RegisterType<IFactoryCustomer, FactoryCustomer>("N");
            retailCustomerFactory.RegisterType<IFactoryCustomer, FactoryCustomerNoTax>("NT");
            retailCustomerFactory.RegisterType<IFactoryCustomer, FactoryCustomerHoliday>("H");

            CorporateCustomerFactory.RegisterType<IFactoryCustomerCorp, 
                                                FactoryCorp1>("corp1");
            CorporateCustomerFactory.RegisterType<IFactoryCustomerCorp,
                                                FactoryCorp>("corp2");
        }
        public static ICustomer CreateRetailCustomer(string type)
        {
            return retailCustomerFactory.Resolve<IFactoryCustomer>(type).Create();
        }
        public static ICustomer CreateCorporateCustomer(string type)
        {
            return CorporateCustomerFactory.Resolve<IFactoryCustomerCorp>(type).Create();
        }
    }
    //Define an interface for creating an object
    public interface IFactoryCustomer
    {
        IDelivery CreateDeliver();
        IDiscount CreateDiscount();
        ITax CreateTax();
        ICustomer Create();
    }
   
    public  class FactoryCustomer : IFactoryCustomer
    {
        // this is the factory method
        //lets a class defer instantiation it uses to subclasses
        public virtual ICustomer Create()
        {
            return new Customer(CreateTax(),
                                CreateDiscount(),
                                CreateDeliver());
        }

        public virtual IDelivery CreateDeliver()
        {
            // query the recent courier fees
            // query courier  delivery
            return new Courier();
        }

        public virtual IDiscount CreateDiscount()
        {
            return new NoDiscount();
        }

        public virtual ITax CreateTax()
        {
            // what is tax query db
            // query online service
            return new LocalTax();
        }
    }
    //but let subclasses decide which class to instantiate
    public class FactoryCustomerNoTax : FactoryCustomer
    {
        public override ITax CreateTax()
        {
            return new NoTax();
        }
    }
    public class FactoryCustomerHoliday : FactoryCustomer
    {
        public override IDiscount CreateDiscount()
        {
            return new HoliDayDiscount();
        }
    }

    public interface IFactoryCustomerCorp : IFactoryCustomer
    {
        ICorporateType CreateCorptype();
    }
    public class FactoryCorp : IFactoryCustomerCorp
    {
        public virtual ICustomer Create()
        {
            return new CorpCustomer(CreateTax(),
                                CreateDiscount(),
                                CreateDeliver(),
                                CreateCorptype());
        }

        public virtual ICorporateType CreateCorptype()
        {
            return new PropCorp();
        }

        public virtual IDelivery CreateDeliver()
        {
            // query the recent courier fees
            // query courier  delivery
            return new Courier();
        }

        public virtual IDiscount CreateDiscount()
        {
            return new NoDiscount();
        }

        public virtual ITax CreateTax()
        {
            // what is tax query db
            // query online service
            return new LocalTax();
        }
    }

    public class FactoryCorp1 : FactoryCorp
    {
        public override ICorporateType CreateCorptype()
        {
            return  new  PublicCorp();
        }
    }
}
