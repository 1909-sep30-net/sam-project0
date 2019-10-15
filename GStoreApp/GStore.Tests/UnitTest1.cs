using System;
using Xunit;
using GStoreApp.ConsoleApp;
using GStoreApp.Library;
using DB.Repo;

namespace GStore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void InputCheckShouldCheck()
        {
            Menu menu = new Menu();

            int finalInput1 = menu.InputCheckInt(1, 1);
            int finalInput2 = menu.InputCheckInt(2, 1);
            int finalInput3 = menu.InputCheckInt(3, 1);
            int finalInput4 = menu.InputCheckInt(4, 1);
            int finalInput5 = menu.InputCheckInt(1, 2);
            int finalInput6 = menu.InputCheckInt(2, 2);

            Assert.True(finalInput1 == 1);
            Assert.True(finalInput2 == 2);
            Assert.True(finalInput3 == 3);
            Assert.True(finalInput4 == 4);
            Assert.True(finalInput5 == 1);
            Assert.True(finalInput6 == 2);

        }
    }
}
