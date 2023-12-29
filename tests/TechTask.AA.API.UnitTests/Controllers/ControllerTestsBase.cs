using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using TechTask.AA.API.Controllers;

namespace TechTask.AA.API.Tests.Controllers
{
    public class ControllerTestsBase<T>
        where T : BaseController
    {
        protected Mock<IMediator> _mockMediator;
        protected Mock<IMapper> _mockMapper;
        protected T _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();

            _controller = Activator.CreateInstance(typeof(T), new object[] { _mockMediator.Object, _mockMapper.Object }) as T;
        }
    }
}
