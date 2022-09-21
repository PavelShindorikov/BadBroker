import { ref } from 'vue';
import axios from "axios";
import { localeString } from "@/common/helpers";
export const useTrade = () => {
    const bestRevenue = ref(null);
    const error = ref(null);
    const submitted = ref(false);
    const loading = ref(null);
    const ratesDTO = ref(null);
    /** mapping api data to dto **/
    const toRatesDTO = (rates) => {
        const keys = Object.keys(rates[0]);
        let index;
        return rates.reduce((acc, cur) => {
            if (!acc['currencies'])
                acc['currencies'] = [];
            keys.forEach(key => {
                if (key === 'date') {
                    (acc['dates'] = acc['dates'] || []).push(localeString(new Date(cur[key])));
                }
                else {
                    index = acc.currencies.findIndex((x) => x['name'] === key.toUpperCase());
                    if (index < 0) {
                        acc.currencies.push({ name: key.toUpperCase(), values: [] });
                        index = acc.currencies.length - 1;
                    }
                    acc.currencies[index].values.push(cur[key]);
                }
            });
            return acc;
        }, {});
    };
    const getBestRevenue = async (request) => {
        loading.value = true;
        submitted.value = true;
        let startDate = localeString(request.startDate);
        let endDate = localeString(request.endDate);
        try {
            const response = await axios.get(`rates/best?startDate=${startDate}&endDate=${endDate}&money=${request.moneyUSD}`);
            bestRevenue.value = (response.data.revenue > 0) ? {
                buyDate: localeString(new Date(response.data.buyDate)),
                sellDate: localeString(new Date(response.data.sellDate)),
                tool: response.data.tool,
                revenue: Number(response.data.revenue.toFixed(2))
            } : null;
            ratesDTO.value = toRatesDTO(response.data.rates);
        }
        catch (e) {
            error.value = e.message;
        }
        finally {
            loading.value = false;
        }
    };
    return { ratesDTO, bestRevenue, error, submitted, loading, getBestRevenue };
};
