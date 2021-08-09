using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryAndAbstractFactory
{
    //"Define an interface for creating an object, but let subclasses decide which class to instantiate. 
    //The Factory method lets a class defer instantiation it uses to subclasses."
    public interface ICustomer
    {
        decimal TotalBill();
      
    }
    public interface ICorporateType
    {
        decimal CalculateCredit();
    }
    public class PropCorp : ICorporateType
    {
        public decimal CalculateCredit()
        {
            throw new NotImplementedException();
        }
    }
    public class PublicCorp : ICorporateType
    {
        public decimal CalculateCredit()
        {
            throw new NotImplementedException();
        }
    }
    public interface IDiscount
    {
        decimal Calculate();
    }
    public interface ITax
    {
        decimal Calculate();
    }
    public interface IDelivery
    {
        decimal Calculate();
    }
    // No discount
    public class NoDiscount : IDiscount
    {
        public decimal Calculate()
        {
            throw new NotImplementedException();
        }
    }
    // Holiday Discount
    public class HoliDayDiscount : IDiscount
    {
        public decimal Calculate()
        {
            // lot of logic
            return 5;
        }
    }
    // Sat and Sunday
    public class WeeklyDiscount : IDiscount
    {
        public decimal Calculate()
        {
            // lot of logic
            return 1;
        }
    }
    
    // No tax
    public class NoTax : ITax
    {
        public decimal Calculate()
        {
            throw new NotImplementedException();
        }
    }
    // Local taxes
    public class LocalTax : ITax
    {
        public decimal Calculate()
        {
            throw new NotImplementedException();
        }
    }
    // State Taxes
    public class StateTax : ITax
    {
        public decimal Calculate()
        {
            throw new NotImplementedException();
        }
    }
    
    // Hand delivery
    public class HandDelivery : IDelivery
    {
        public decimal Calculate()
        {
            throw new NotImplementedException();
        }
    }
    // Courier
    public class Courier : IDelivery
    {
        public decimal Calculate()
        {
            throw new NotImplementedException();
        }
    }

    public class Customer : ICustomer
    {
        private ITax _tax = null;
        private IDiscount _Discount = null;
        private IDelivery _Delivery = null;
        public Customer(ITax tax , IDiscount discount 
                        , IDelivery delivery)
        {
            _tax = tax;
            _Discount = discount;
            _Delivery = delivery;
        }
        public decimal TotalPurchase { get; set; }
        public  virtual decimal TotalBill()
        {
            // there is some
            // some more
            // big calculation
            return (TotalPurchase +
                _tax.Calculate() +
                _Delivery.Calculate()) 
                - _Discount.Calculate();
                                    
        }
    }

    public class CorpCustomer : Customer
    {
        private ICorporateType _corptype = null;
        public CorpCustomer(ITax tax, IDiscount discount
                        , IDelivery delivery,
                        ICorporateType corptype) 
                        : base(tax,discount,delivery)
        {
            _corptype = corptype;
        }
        public override decimal TotalBill()
        {
            return base.TotalBill() - _corptype.CalculateCredit() ;
        }
    }
    public class DiscountedCustomer : ICustomer
    {
       

        public  decimal TotalBill()
        {
            // logic for calculation
            return 5;
        }
    }

    public class GoldCustomer : ICustomer
    {
       

        public decimal TotalBill()
        {
            throw new NotImplementedException();
        }
    }

    public class SpecialCustomer : ICustomer
    {
        

        public decimal TotalBill()
        {
            throw new NotImplementedException();
        }
    }
}
