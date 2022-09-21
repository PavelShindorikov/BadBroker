export interface BestRevenueRequest {
  startDate: Date | undefined,
  endDate: Date | undefined,
  moneyUSD: number | undefined
}

export interface Rate {
  key: string,
  value: number | Date
}

export interface BestRevenue {
  buyDate: Date | string,
  sellDate: Date | string,
  tool: string,
  revenue: number
}

export interface BestRevenueResponse extends BestRevenue {
  rates: Rate[]
}

export interface RatesDTO {
  dates: string[],
  currencies: [{
    name: string,
    values: number[]
  }],
}
