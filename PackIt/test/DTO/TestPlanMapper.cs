// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Test.DTO
{
    using System.IO;
    using System.Text.Json;
    using Xunit;
    using PackItLib.Plan;
    using PackIt.DTO;

    /// <summary> (Unit Test Fixture) a mapper for plans. </summary>
    public class TestPlanMapper
    {
        /// <summary> The mapper under test. </summary>
        private readonly PlanMapper mapper;

        /// <summary> Setup for all unit tests here. </summary>
        public TestPlanMapper()
        {
            this.mapper = new();
            Assert.NotNull(this.mapper);
        }

        /// <summary> (Unit Test Method) Convert a Plan to it's DTO. </summary>
        [Fact]
        public void ConvertPlanToDto()
        {
            var text = File.ReadAllText("DTO/TestData/plan.json");
            var plan = JsonSerializer.Deserialize<Plan>(text);
            var dto = this.mapper.ConvertToDto(plan);

            Assert.Equal(dto.PlanId, plan.PlanId);
            int minLevel = -1;
            Assert.Equal(dto.Stages.Count, plan.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in dto.Stages)
            {
                Assert.Equal(stage.PlanId, plan.PlanId);
                Assert.True(minLevel <= (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.Equal(stage.Limits.Count, plan.Stages[stageIndex].Limits.Count);
                int limitIndex = 0;
                foreach (var limit in stage.Limits)
                {
                    Assert.Equal(limit.PlanId, plan.PlanId);
                    Assert.Equal(limit.StageLevel, stage.StageLevel);
                    Assert.Equal(limit.LimitIndex, limitIndex);
                    ++limitIndex;
                }
                ++stageIndex;
            }
        }

        /// <summary> (Unit Test Method) Convert a Plan to it's DTO. </summary>
        [Fact]
        public void ConvertDtoToPlan()
        {
            var text = File.ReadAllText("DTO/TestData/plan.json");
            var plan = JsonSerializer.Deserialize<Plan>(text);
            var dto = this.mapper.ConvertToDto(plan);
            var data = this.mapper.ConvertToData(dto);

            Assert.Equal(data.PlanId, plan.PlanId);
            int minLevel = -1;
            Assert.Equal(data.Stages.Count, plan.Stages.Count);
            int stageIndex = 0;
            foreach (var stage in data.Stages)
            {
                Assert.True(minLevel <= (int)stage.StageLevel);
                minLevel = (int)stage.StageLevel;

                Assert.Equal(stage.Limits.Count, plan.Stages[stageIndex].Limits.Count);
                ++stageIndex;
            }
        }
    }
}
