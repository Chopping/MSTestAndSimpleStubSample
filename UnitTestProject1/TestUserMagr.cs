
using System;
using App1;
using App1.Magr;
using Etg.SimpleStubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class TestUserMagr
    {
        // 待测试对象-> uMagr  Mock对象->uMagr.UDao(IUserDao)
        private UserMagr uMagr;        

        [TestInitialize]
        public void BeforeTestInitialize()
        {
            uMagr = new UserMagr();
        }

        
        [TestMethod]
        public void TestCallSequence()
        {

            // 新建mock对象
            var stub = new StubIUserDao(MockBehavior.Strict)
                .CreateUser((p2) => { return null == p2 ? 0 : 1; }, Times.Once)// 限定最多调用一次
                .CreateUser((p2) => { return null == p2 ? 0 : 1; }, Times.Twice)// 限定最多调用两次
                .CreateUser((p2) => { return null == p2 ? 0 : 1; }, Times.Forever);// 解除调用次数上限设置
                                                                                   // 如果不执行上述最后一行，那么stub对象只能执行CreateUser 两次（取决于最后一条的限定），执行超出次数会抛异常

            // 将Mock对象赋值给依赖对象，从而解除依赖对象对测试的干预
            uMagr.UDao = stub;
            // 从这开始执行测试 类似 EasyMock->replayAll(); 
            Assert.AreEqual(1, uMagr.CreateUser(new User() { Username = "123", Password = "123" }));
            Assert.AreEqual(1, uMagr.CreateUser(new User() { Username = "123", Password = "123" }));
            Assert.AreEqual(1, uMagr.CreateUser(new User() { Username = "123", Password = "123" }));
        }

        [TestMethod]
        public void TestMethod_WithReturnType_WithParameters()// 测试Mock对象的函数带参数的调用
        {
            // 设置一些初始变量
            int Expect = 1;
            string Username = null;
            string Password = null;
            // 用于判断是否修改的对象
            User user = new User() { Username = Username, Password = Password };
            // 开始构建Mock
            var stub = new StubIUserDao()
                .CreateUser((u) => { user = u; return Expect; });
            uMagr.UDao = stub;
            // replayAll()
            Assert.AreEqual(Expect, uMagr.CreateUser(new User() { Username = "test" }));
            Assert.AreEqual("test", user.Username);
        }

        [TestMethod]
        public void TestThatMethodStubCanBeOverwritten()// 测试Mock对象的方法是否支持重写
        {
            // 构建一个Mock对象
            var stub = new StubIUserDao()
                .CreateUser(u => 1);
            // 重写其调用的方法，如果需要重写则需要加上overwrite:true,否则将不会重写
            stub.CreateUser(u => 2, overwrite: true);
            uMagr.UDao = stub;
            // replayAll()
            Assert.AreEqual(2, uMagr.CreateUser(new User()));
        }

        [TestMethod]
        [ExpectedException(typeof(SimpleStubsException))]
        public void TestMethod_WithReturnType_WithParameters_DefaultBehavior_Strict()// 展示 MockBehavior为Strict下的不同之处，于下面的method一起比对观察
        {
            // 构建一个不带函数定义的Mock对象（存根）
            var stub = new StubIUserDao(MockBehavior.Strict);
            uMagr.UDao = stub;
            // 调用方法的时候，Strict模式下会抛出异常，因为没有定义调用函数
            Assert.AreEqual(0, uMagr.CreateUser(null));
        }

        [TestMethod]
        public void TestMethod_Void_WithNoParameters_DefaultBehavior_Loose()
        {
            // 构建一个不带函数定义的Mock对象（存根），默认下MockBehavior为Loose
            var stub = new StubIUserDao();
            uMagr.UDao = stub;
            // 调用方法的时候，Loose模式下不会抛出异常，并且返回函数返回值的默认值 null | 默认基本类型值 | 
            Assert.AreEqual(0, uMagr.CreateUser(null));
        }       
    }
}
