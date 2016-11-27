using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorks
{
    class Helpers
    {
        public void GridsParser (List<IWebElement> gridCells, List<IWebElement> headerCells, List<string> outputCollection, string columnName) {

            string index = "";

            foreach (IWebElement element in headerCells)
            {
                if (element.GetAttribute("innerText") == columnName)
                {
                    index = element.GetAttribute("cellIndex");
                }
            }


            foreach (IWebElement cell in gridCells)
            {
                if (cell.GetAttribute("cellIndex") == index)
                {
                    outputCollection.Add(cell.GetAttribute("innerText"));                    
                }
            }
        }

        public void CheckOrder(List<string> list) {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (String.Compare(list[i], list[i + 1]) > 0)
                {
                    throw new System.Exception("Order isn't correct");
                }
            }
        }
    }
}
