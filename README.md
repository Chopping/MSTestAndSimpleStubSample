# MSTestAndSimpleStubSample

## 目录

1. [项目背景](#项目背景):
1. [项目环境](#项目环境):
1. [文件结构](#文件结构):
1. [框架简介](#框架简介):
1. [测试结果](#测试结果):
1. [作者结语](#作者结语):

### 项目背景
<a id="项目背景" />

&nbsp;&nbsp;作者近期刚好开发UWP的项目，然后想对项目添加测试模块，寻求了多种测试方案加以多方面考虑，最终敲定使用 `MSTest` 的UnitTest框架 + `SampleStub` 的Mock框架 构建项目的测试部分。这两种框架都是来自微软原生：
* [MSTest MSDN传送门](https://docs.microsoft.com/en-us/visualstudio/test/unit-test-your-code)
* [SampleStub Github传送门](https://github.com/Microsoft/SimpleStubs)

### 文件结构：
<a id="文件结构" />

&nbsp;&nbsp;本项目主要包含两个子项目：
* UnitTestProject1 单元测试应用项目
* App1 被测试的UWP项目

&nbsp;&nbsp;其中UnitTestProject1引用了App1的Model、Dao、Magr（Controller）模块。着重介绍几个重要的类文件：
* User.cs(App1) 简单的用户模型类
* IUserDao.cs(App1) 简单的数据访问层类
* UserMagr.cs(App1) 简单的业务逻辑层类
* TestMagr.cs(UnitTestProject1) 简单的测试UserMagr类的测试类

### 项目环境
<a id="项目环境"/>

* Windows 10 OS
* Windows 10 SDK 10.0.15860
* Visual Studio 2017
* MSTest Framework 1.2.0
* Etg.SimpleStubs 2.4.6

### 框架简介：
<a id="框架简介" />

* 驱动程序：MSTest Framework 提供的Runner + TestAdapter
* 桩程序：SimpleStub 提供的 `编译时` 的Mock框架(据官方称目前只能在编译期间自动生成IXxx的接口的Mock类)

### 测试结果:
<a id="测试结果" />

```
Test Running ...

TestCallSequence Passed!
TestMethod_WithReturnType_WithParameters Passed!
TestThatMethodStubCanBeOverwritten Passed!
TestMethod_WithReturnType_WithParameters_DefaultBehavior_Strict Passed!
TestMethod_Void_WithNoParameters_DefaultBehavior_Loose Passed!

TestPlayList all tests succeed!
```

### 作者结语:
<a id="作者结语" />

&nbsp;&nbsp;作为一个刚入测试的小菜鸡，作者在结合使用这两个框架的时候也遇到了很多坑，下面简单列举一些萌新们可能会踩的坑：

1. UWP 新建一个UnitTest项目？
>答：UWP 的单元测试项目优选 `单元测试应用` 类型的项目，即新建一个`单元测试应用`的项目，并且在其中书写自己的测试

2. 为什么在新建的UnitTestProject内新建测试文件不会在测试列表中显示？
>答：测试文件目前使用的是注解机制，其中对作为TestFixture的测试类以及测试方法都是有访问权限要求的，即必须都为 `public` 的 ，并且测试方法 `Method` 必须为 `public void` 类型的。

3. 测试常用的东西: `Assert` 类的诸多方法，Such as: `AreEqual` `AreSame` `IsTrue` 等

4. 如何在目前的Unit测试引入Mock框架？
>答：目前的情况，作者尝试过许多框架，出于作者水平，这些框架（NUnit Template 不支持、XUnit 换成Library无法识别测试)在作者的UWP上表现都不太好。目前发现表现最好的是微软去年年末推出的 `SimpleStub` 框架，然后作者也推荐使用这个，因为作者个人认为这个是目前与 `MSTest` 兼容最好的Mock框架。

5. `SimpleStub` 如何使用？
>答：Nuget -> download SimpleStub nuget package 。然后环境就已经搭建好了，选择你的测试项目进行生成，此时框架会自动在编译期间在项目obj目录下生成一个 `SimpleStub.generate.cs` 的文件。利用这个文件生成一个 `StubXxxx` 的Mock对象进而实现测试，详细用法见样例代码（ `UnitTestProject1` 内的 `TestUserMagr.cs`）

**Tips:**
* 这里补充说明下，如果项目生成后在obj目录下没找到 `SimpleStub.generate.cs` 文件，说明生成 `SimpleStub.generate.cs` 文件失败了，此时应该去查看*Console*(输出控制台)的输出内容，挖掘出错误的原因，解决异常后重新生成下即可！
* 同时需要多留意 `SimpleStub` 框架为我们生成的 `StubXxx` Mock对象的一些方法构建，因为这与构建的Mock对象的 `MockBehavior` 息息相关，详细请见示例代码
