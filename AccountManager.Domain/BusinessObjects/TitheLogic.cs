using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain.BusinessObjects
{
    public class TitheLogic
    {
        public decimal CalculateTithe(decimal income)
        {
            return  10 / 100 * income;
        }

        //This method is to show a total of all payments of tithe for a month
        public decimal TotalTithePerMonth()
        {
            //so take 10% of every income and add them for the total of every month
            //should I do a linq query to get all income for a particular month and
            //then get the tithe for every income?
            decimal tithe = 20M;
            return tithe;
            
        }
    }
}
