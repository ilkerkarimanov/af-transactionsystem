﻿using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class TransferId_Should
    {
        [Fact]
        public void ReturnsObject_When_Valid()
        {
            var obj = new TransferId(Guid.NewGuid());
            Assert.IsType<TransferId>(obj);
        }

        [Fact]
        public void ThrowsException_When_Empty()
        {
            Assert.Throws<ArgumentException>(() => new TransferId(Guid.Empty));
        }
    }
}
