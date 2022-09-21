<template>
  <el-header>
    <h2>Trade page</h2>
  </el-header>

  <el-card class="box-card">
    <InputForm @onGetBestRevenue="getBestRevenue" />
  </el-card>
  <div v-if="submitted">
    <el-card class="box-card" v-loading="loading">
      <template v-if="ratesDTO">
        <el-alert
          v-if="error"
          title="Get data error"
          type="error"
          :description="error"
          show-icon
          @click="closeError"
          effect="dark"
        />
        <TradeResult v-else :best-revenue="bestRevenue" :rates="ratesDTO"/>
      </template>
    </el-card>
  </div>


</template>

<script setup lang="ts">
  /** components **/
  import InputForm from "@/components/InputForm.vue";
  import TradeResult from "@/components/TradeResult.vue"

  /** modules **/
  import { useTrade } from "@/use/trade";

  /** properties **/
  const { ratesDTO, bestRevenue, submitted, loading, error, getBestRevenue } = useTrade();

  /** methods **/
  const closeError = () => {
    error.value = null;
    submitted.value = false;
  }
</script>

<style scoped>
.el-alert{
  margin: 20px 0 0;
}
.el-alert:first-child{
  margin: 0;
}
</style>