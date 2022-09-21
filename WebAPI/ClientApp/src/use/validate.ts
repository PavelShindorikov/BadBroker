import type {BestRevenueRequest} from "@/models/models";
import { reactive, ref } from "vue";
import type {FormInstance} from "element-plus";

const dateName = (val:string) => val === 'startDate' ? 'Start' : 'End'
const reversDateField = (field: string) => field === 'startDate' ? 'endDate' : 'startDate'


export const useValidate = ( form: BestRevenueRequest ) => {
	const ruleFormRef = ref<FormInstance>()
	const checkDate = ref<boolean>(false)

	const validatePeriod = () => {
		let dateDif = Math.ceil((form.endDate!.getTime() - form.startDate!.getTime())/(1000 * 3600 * 24))
		if (dateDif > 60)
			return 'Period cannot exceed 2 months (60 days)'
		if (dateDif < 2)
			return 'Period cannot be less than 2 days'
		return null
	}

	const validateDate = (rule: any, value: any, callback: any) => {
		checkDate.value = !checkDate.value
		if (!value) {
			callback(new Error(`Please pick the ${dateName(rule.field)} date`))
		} else {
			if (form.endDate && form.startDate) {
				if (form.startDate! > form.endDate!) {
					callback(new Error('Start date must be lesser End date'))
				} else {
					callback(validatePeriod() || undefined)
				}
			} else { callback() }
		}
		if (checkDate.value) {
			ruleFormRef.value?.validateField(reversDateField(rule.field), () => null)
		}
	}

	const validateMoney = (rule: any, value: any, callback: any) => {
		if (!value)
			return callback(new Error('Please input the money'))
		if (!Number(value))
			return callback(new Error('Please input digits'))
		if (value < 1 || value > 1000000)
			return (new Error('Money must be COOL'))
		callback()
	}

	const rules = reactive({
		startDate: [{ validator: validateDate, trigger: 'change' }],
		endDate: [{ validator: validateDate, trigger: 'change' }],
		moneyUSD: [{ validator: validateMoney, trigger: 'blur' }]
	})

	return { rules, ruleFormRef }
}