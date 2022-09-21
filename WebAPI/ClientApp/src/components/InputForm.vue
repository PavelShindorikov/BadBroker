<template>
  <el-form
    class="input_form"
    label-width="120px"
    status-icon
    :model="formData"
    ref="ruleFormRef"
    :rules="rules"
  >
    <el-form-item label="Start date" prop="startDate">
      <el-date-picker
        v-model="formData.startDate"
        type="date"
        :disabled-date="disabledDate"
        placeholder="Start date"
      />
    </el-form-item>
    <el-form-item label="End date" prop="endDate">
      <el-date-picker
        v-model="formData.endDate"
        type="date"
        :disabled-date="disabledDate"
        placeholder="End date"
      />
    </el-form-item>
    <el-form-item label="Money USD" prop="moneyUSD">
      <el-input
        class="w-30"
        v-model.number="formData.moneyUSD"
      />
    </el-form-item>
    <el-form-item>
      <el-button
        type="primary"
        @click="submitForm()"
      >
        Get best revenue
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
  /** types **/
  import type { BestRevenueRequest } from "@/models/models";

  /** modules **/
  import { reactive } from "vue";
  import { useValidate } from "@/use/validate";

  /** properties **/
  const formData = reactive<BestRevenueRequest>({
    startDate: undefined,
    endDate: undefined,
    moneyUSD: undefined
  })
  const { rules, ruleFormRef } = useValidate(formData)

  /** emits **/
  const emit = defineEmits<{
    (e:'onGetBestRevenue', formData: BestRevenueRequest) : void
  }>()

  /** methods **/
  const disabledDate = (time: Date) => {
    return time.getTime() > Date.now() || time.getTime() < new Date('1999-01-01').getTime()
  }
  const submitForm = () => {
    ruleFormRef.value!.validate((valid) => {
      if (valid) {
        emit('onGetBestRevenue', formData);
        ruleFormRef.value!.resetFields();
      } else {
        return false
      }
    })
  }
</script>

<style scoped>

</style>