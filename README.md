# MSTestAndSimpleStubSample

## 目录

1. [项目背景]()：
1. [文件结构]()：
1. [框架简介]()：
1. [测试结果]()：
1. [作者结语]()：

### 项目背景

&nbsp;&nbsp;作者近期刚好开发UWP的项目，然后想对项目添加测试模块，寻求了多种测试方案加以多方面考虑，最终敲定使用 `MSTest` 的UnitTest框架 + `SampleStub` 的Mock框架 构建项目的测试部分。这两种框架都是来自微软原生：
* [MSTest MSDN传送门](https://docs.microsoft.com/en-us/visualstudio/test/unit-test-your-code)
* [SampleStub Github传送门](https://github.com/Microsoft/SimpleStubs)

### 文件结构：

&nbsp;&nbsp;本项目主要包含两个子项目：
* UnitTestProject1 单元测试应用项目
* App1 被测试的UWP项目

&nbsp;&nbsp;其中UnitTestProject1引用了App1的Model、Dao、Magr（Controller）模块。着重介绍几个重要的类文件：
* User.cs(App1) 简单的用户模型类
* IUserDao.cs(App1) 简单的数据访问层类
* UserMagr.cs(App1) 简单的业务逻辑层类
* TestMagr.cs(UnitTestProject1) 简单的测试UserMagr类的测试类

### 框架简介：

* 驱动程序：MSTest Framework 提供的Runner + TestAdapter
* 桩程序：SimpleStub 提供的 `编译时` 的Mock框架(据官方称目前只能在编译期间自动生成IXxx的接口的Mock类)

### 测试结果:

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

&nbsp;&nbsp;作为一个刚入测试的小菜鸡，作者在结合使用这两个框架的时候也遇到了很多坑，下面简单列举一些萌新们可能会踩的坑：

1. UWP 如何做UnitTest：
>答：UWP 
