// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using System.Text.Json;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.Plan;

    /// <summary> (Unit Test Fixture) a mapper for plans. </summary>
    [TestFixture]
    public class TestPlanMapper
    {
        /// <summary> The mapper under test. </summary>
        private PlanMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.mapper = new();
            Assert.IsNotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) Convert a Plan to it's DTO. </summary>
        [Test]
        public void ConvertPlanToDto()
        {
            var text = File.ReadAllText("DTO/TestData/plan.json");
            var plan = JsonSerializer.Deserialize<Plan>(text);
            var dto = this.mapper.ConvertToDto(plan);

            Assert.AreEqual(plan.PlanId, dto.PlanId);
            int minLevel = -1;
            Assert.AreEqual(plan.Stages.Count, dto.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Stages)
            {
                Assert.AreEqual(plan.PlanId, stage.PlanId);
                Assert.LessOrEqual(minLevel, (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(plan.Stages[stageIndex].Limits.Count, stage.Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.AreEqual(plan.PlanId, limit.PlanId);
                    Assert.AreEqual(stage.StageLevel, limit.StageLevel);
                    Assert.AreEqual(limitIndex, limit.LimitIndex);
                    ++limitIndex;
                }
                ++stageIndex;
            }
        }

        /// <summary> (Unit Test Method) Convert a Plan to it's DTO. </summary>
        [Test]
        public void ConvertDtoToPlan()
        {
            var text = File.ReadAllText("DTO/TestData/plan.json");
            var plan = JsonSerializer.Deserialize<Plan>(text);
            var dto = this.mapper.ConvertToDto(plan);
            var data = this.mapper.ConvertToData(dto);

            Assert.AreEqual(plan.PlanId, data.PlanId);
            int minLevel = -1;
            Assert.AreEqual(plan.Stages.Count, data.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in data.Stages)
            {
                Assert.LessOrEqual(minLevel, (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(plan.Stages[stageIndex].Limits.Count, stage.Limits.Count);
                ++stageIndex;
            }
        }
    }
}
