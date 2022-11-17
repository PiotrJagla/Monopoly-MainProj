using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;

namespace UnitTests
{
    [TestClass]
    public class ValidationIndexTests
    {
        [TestMethod]
        public void IsWithin2DArrayTest()
        {
            Point2D ArraySize = new Point2D(13, 13);
            Point2D Index = new Point2D(12, 13);

            Assert.IsTrue(ValidateIndex.IsWithin2DArray(Index, ArraySize) == false);
            Index.x = -1; Index.y = 6;
            Assert.IsTrue(ValidateIndex.IsWithin2DArray(Index, ArraySize) == false);
            Index.x = 0; Index.y = 6;
            Assert.IsTrue(ValidateIndex.IsWithin2DArray(Index, ArraySize) == true);

            ArraySize.x = 14; ArraySize.y = 8;
            Index.x = 11; Index.y = 6;
            Assert.IsTrue(ValidateIndex.IsWithin2DArray(Index, ArraySize) == true);

            ArraySize.x = 14; ArraySize.y = 21;
            Index.x = 17; Index.y = 19;
            Assert.IsTrue(ValidateIndex.IsWithin2DArray(Index, ArraySize) == false);

            Index.x = 4; Index.y = 15;
            Assert.IsTrue(ValidateIndex.IsWithin2DArray(Index, ArraySize) == true);
        }
    }
}
