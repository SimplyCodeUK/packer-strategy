// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItTest.DTO
{
    using System.IO;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using PackIt.DTO;
    using PackIt.DTO.DtoPlan;
    using PackIt.Models.Plan;

    /// <summary>   (Unit Test Fixture) a mapper for plans. </summary>
    [TestFixture]
    public class TestPlanMapper
    {
        /// <summary>   (Unit Test Method) Convert a Plan to it's DTO. </summary>
        [Test]
        public void ConvertPlan()
        {
            string text = File.ReadAllText("DTO/TestData/plan.json");
            Plan plan = JsonConvert.DeserializeObject<Plan>(text);

            DtoPlan dto = PlanMapper.Convert(plan);

            Assert.AreEqual(dto.PlanId, plan.PlanId);
            int minLevel = -1;
            Assert.AreEqual(dto.Stages.Count, plan.Stages.Count);
            int stageIndex = 0;
            foreach (DtoStage stage in dto.Stages)
            {
                Assert.AreEqual(stage.PlanId, plan.PlanId);
                Assert.Greater((int)stage.StageLevel, minLevel);
                minLevel = (int)stage.StageLevel;

                Assert.AreEqual(stage.Limits.Count, plan.Stages[stageIndex].Limits.Count);
                int limitIndex = 0;
                foreach (DtoLimit limit in stage.Limits)
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
