using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.UnitTests
{
    [TestFixture]
    internal class CaculaterTests
    {
        [TestCase(1,2,3)]
        [TestCase(2, 3, 5)]
        [TestCase(4, 6, 10)]
        [TestCase(1, 5, 6)]
        public void Add_ValidInput_ReturnSuccess(int a, int b, int expertedResult) {
            
            var caculater=new Caculater();
            var ac=caculater.Add(a, b);

            Assert.That( ac, Is.EqualTo(expertedResult));
        }
        [TestCase(1, 2, -1)]
        [TestCase(2, 3, -1)]
        [TestCase(4, 6, -2)]
        [TestCase(int.MaxValue, int.MinValue, ((long)int.MaxValue * 2 +1))]
        public void Sub_ValidInput_ReturnSuccess(int a, int b, long expertedResult)
        {

            var caculater = new Caculater();
            var ac = caculater.Sub(a, b);

            Assert.That(ac, Is.EqualTo(expertedResult));
        }
        [TestCase(1, 2, 2)]
        [TestCase(2, 3, 6)]
        [TestCase(4, 6, 24)]
        [TestCase(int.MaxValue, int.MinValue, ((long)int.MaxValue * int.MinValue))]
        public void Mul_ValidInput_ReturnSuccess(int a, int b, long expertedResult)
        {

            var caculater = new Caculater();
            var ac = caculater.Mul(a, b);

            Assert.That(ac, Is.EqualTo(expertedResult));
        }

        [TestCase(1, 2, (double)1/2)]
        [TestCase(2, 3, (double)2 / 3)]
        [TestCase(4, 6, (double)4 / 6)]
        //[TestCase(int.MaxValue, int.MinValue, ((double)int.MaxValue * int.MinValue))]
        public void Div_ValidInput_ReturnSuccess(int a, int b, Double expertedResult)
        {

            var caculater = new Caculater();
            var ac = caculater.Div(a, b);

            Assert.That(ac, Is.EqualTo(expertedResult));
        }

        [TestCase(1, 0, "Không chia được cho 0")]
        //[TestCase(int.MaxValue, int.MinValue, ((double)int.MaxValue * int.MinValue))]
        public void Div_Notzero_ReturnSuccess(int a, int b, string expertedResult)
        {

            var caculater = new Caculater();
            var ac =Assert.Throws<Exception>(()=>caculater.Div(a, b));
            Assert.That(ac.Message,Is.EqualTo(expertedResult));

        }
    }
}
