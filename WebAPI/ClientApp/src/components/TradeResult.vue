<template>
  <div v-if="bestRevenue">
    <h2>The best case for this period is {{ bestRevenue.tool }}</h2>
    <span>
      Date for buy is {{ bestRevenue.buyDate }}<br>
      Date for sell is {{ bestRevenue.sellDate }}<br>
      Revenue is ${{ bestRevenue.revenue }}<br>
    </span>
    <RatesChart
      :index="0"
      :labels="labels"
      :bestRevenue="bestRevenue"
      :dataset="bestCase"
      :key="0"
    />
  </div>
  <div v-else>
    <h2>This period not have condition for revenue</h2>
    <span>
      Try choose another period or bet more money
    </span>
  </div>
  <h2>Chart data without best condition</h2>
  <RatesChart
    v-for="(value, index) in otherData"
    :index="index+step"
    :labels="labels"
    :dataset="value"
    :key="index+step"
  />
</template>


<script setup lang="ts">
/** components **/
import RatesChart from "@/components/UI/RateChart.vue";

/** types **/
import type {BestRevenue, RatesDTO} from "@/models/models";

/** modules **/
import { computed } from "vue";

/** properties **/
const props = defineProps<{
  bestRevenue?: BestRevenue,
  rates: RatesDTO
}>();
const step = (props.bestRevenue) ? 1 : 0;

/** computed **/
const labels = computed(() => props.rates.dates)

const bestCase = computed(() =>
  props.rates.currencies.find(currency => currency.name === props.bestRevenue?.tool))

const otherData = computed(() =>
  props.rates.currencies.filter(currency => currency.name !== props.bestRevenue?.tool))
</script>