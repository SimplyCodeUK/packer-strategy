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
            Assert.That(this.mapper, Is.Not.Null);
        }

        /// <summary> (Unit Test Method) Convert a Plan to it's DTO. </summary>
        [Test]
        public void ConvertPlanToDto()
        {
            var text = File.ReadAllText("DTO/TestData/plan.json");
            var plan = JsonSerializer.Deserialize<Plan>(text);
            var dto = this.mapper.ConvertToDto(plan);

            Assert.That(dto.PlanId, Is.EqualTo(plan.PlanId));
            int minLevel = -1;
            Assert.That(dto.Stages.Count, Is.EqualTo(plan.Stages.Count));
            int stageIndex = 0;
            foreach (var stage in dto.Stages)
            {
                Assert.That(stage.PlanId, Is.EqualTo(plan.PlanId));
                Assert.That(minLevel, Is.LessThanOrEqualTo((int)stage.StageLevel));
                minLevel = (int)stage.StageLevel;

                Assert.That(stage.Limits.Count, Is.EqualTo(plan.Stages[stageIndex].Limits.Count));
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.That(limit.PlanId, Is.EqualTo(plan.PlanId));
                    Assert.That(limit.StageLevel, Is.EqualTo(stage.StageLevel));
                    Assert.That(limit.LimitIndex, Is.EqualTo(limitIndex));
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

            Assert.That(data.PlanId, Is.EqualTo(plan.PlanId));
            int minLevel = -1;
            Assert.That(data.Stages.Count, Is.EqualTo(plan.Stages.Count));
            int stageIndex = 0;
            foreach (var stage in data.Stages)
            {
                Assert.That(minLevel, Is.LessThanOrEqualTo((int)stage.StageLevel));
                minLevel = (int)stage.StageLevel;

                Assert.That(stage.Limits.Count, Is.EqualTo(plan.Stages[stageIndex].Limits.Count));
                ++stageIndex;
            }
        }
    }
}
