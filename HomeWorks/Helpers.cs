using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorks
{
    class Helpers
    {

        public void GetInnerText (List<IWebElement> gridCells, List<string> outputCollection, Int32 columnIndex) {
            foreach (IWebElement cell in gridCells)
            {
                outputCollection.Add(cell.GetAttribute("innerText"));         
            }
        }


        public Int32 GetColumnIndex(List<IWebElement> headerCells, string columnName) {
            Int32 i = 0;
            foreach (IWebElement element in headerCells)
            {
                if (element.GetAttribute("innerText") == columnName)
                {
                    i = i + Int32.Parse(element.GetAttribute("cellIndex"));
                }
            }
            return i;
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
