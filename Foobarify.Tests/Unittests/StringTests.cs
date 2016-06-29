using Foobarify.Business.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foobarify.Tests.Unittests
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void FoobarifyString()
        {
            var originalString = "hej hej nej oj oj oj";

            var shouldBe = "hej hej nej FOOojBAR FOOojBAR FOOojBAR";

            var isString = originalString.FooBarify();

            Assert.AreEqual(shouldBe, isString);
        }

        [TestMethod]
        public void FoobarifyStringFirstUpperCasePreserved()
        {
            var originalString = "hej Hej nej Oj oj oj";

            var shouldBe = "hej Hej nej FOOOjBAR FOOojBAR FOOojBAR";

            var isString = originalString.FooBarify();

            Assert.AreEqual(shouldBe, isString);
        }

        [TestMethod]
        public void FoobarifyStringMiddleUpperNOTCasePreserved()
        {
            var originalString = "hej hej nej oO oj oj";

            var shouldBe = "hej hej nej FOOojBAR FOOojBAR FOOojBAR";

            var isString = originalString.FooBarify();

            Assert.AreNotEqual(shouldBe, isString);
        }

        [TestMethod]
        public void FoobarifyStringAddsPrefixAndSuffix()
        {
            var originalString = "hej hej nej oO oj oj";

            var shouldBe = "<span>hej hej nej FOOojBAR FOOojBAR FOOojBAR</span>";

            var isString = originalString.FooBarify("<span>, </span>");

            Assert.AreNotEqual(shouldBe, isString);
        }
    }
}
