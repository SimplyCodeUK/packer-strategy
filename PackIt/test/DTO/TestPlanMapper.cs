// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using Newtonsoft.Json;
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
            this.mapper = new PlanMapper();
            Assert.IsNotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) Convert a Plan to it's DTO. </summary>
        [Test]
        public void ConvertPlan()
        {
            var text = File.ReadAllText("DTO/TestData/plan.json");
            var plan = JsonConvert.DeserializeObject<Plan>(text);
            var dto = this.mapper.ConvertToDto(plan);

            Assert.AreEqual(dto.PlanId, plan.PlanId);
            int minLevel = -1;
            Assert.AreEqual(dto.Stages.Count, plan.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Stages)
            {
                Assert.AreEqual(stage.PlanId, plan.PlanId);
                Assert.Greater((int)stage.StageLevel, minLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(stage.Limits.Count, plan.Stages[stageIndex].Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.AreEqual(limit.PlanId, plan.PlanId);
                    Assert.AreEqual(limit.StageLevel, stage.StageLevel);
                    Assert.AreEqual(limit.LimitIndex, limitIndex);

                    ++limitIndex;
                }

                ++stageIndex;
            }
        }
    }
}
