<template>
  <div>
    <el-button type="info" link @click="toggle">{{ buttonName }}</el-button>
  </div>
  <LineChart
    style="max-height: 400px"
    v-show="visible"
    v-bind="lineChartProps"
  />
</template>

<script setup lang="ts">
  import { LineChart, useLineChart} from "vue-chart-3";
  import { Chart, registerables} from 'chart.js'
  Chart.register(...registerables);

  import {computed, onMounted, ref, watch} from "vue";
  import type { BestRevenue } from "@/models/models";
  import { namedColor } from "@/common/utils"
  import { useToggle } from "@/use/toggle";

  const props = defineProps<{
    index: number
    labels: string[]
    dataset: {
      name: string,
      values: number[]
    }
    bestRevenue?: BestRevenue
  }>();

  const { visible, toggle } = useToggle();
  const buttonName = computed(() =>
    `${!visible.value ? "Show" : "Hide"} chart data for ${props.dataset.name}`
  )

  const bestCase = (ctx: any, value: any) =>
      ctx.p0DataIndex >= props.labels.indexOf(props.bestRevenue!.buyDate as string) &&
      ctx.p1DataIndex <= props.labels.indexOf(props.bestRevenue!.sellDate as string) ?
      value : undefined

  const data = computed(() => ({
    labels: props.labels,
    datasets: [{
      label: props.dataset.name,
      data: props.dataset.values,
      borderColor: namedColor(props.index),
      tension: 0.1,
      segment: (props.bestRevenue) ? {
        borderColor: (ctx: any) => bestCase(ctx, 'rgb(127,255,0)')
      } : undefined,
    }]
  }))

  const options = computed(() => ({
    responsive: true,
    plugins: {
      legend:{
        display: false
      }
    },
  }))

  const { lineChartProps, lineChartRef } = useLineChart({
    chartData: data,
    options,
  });

  onMounted(() => {
      if (props.bestRevenue) toggle()
  })

</script>

<style scoped>
.el-button{
  margin: 10px auto;
}
</style>