using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace PageObjects
{
    [TestFixture]
    public class CartTest : TestBase
    {
        [Test]
        public void Cart()
        {
            for (int i = 0; i < app.data.countOfProducts; i++)
            {
                app.mainPage.Open().SelectProduct();
                app.productPage.SetSize(app.data.size).AddToCart();
                
            }

            app.cart.Open();
            int count = app.cart.CountOfItms();
            for (int i = 1; i <= count; i++)
                app.cart.RemoveItem();
        }
    }
}
