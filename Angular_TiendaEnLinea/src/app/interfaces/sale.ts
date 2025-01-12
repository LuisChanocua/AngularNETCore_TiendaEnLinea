import { Detailsale } from "./detailsale";

export interface Sale {
    idsale? : number,
    documentnumber?: string,
    paytype: string,
    createdAt?: string,
    totalText: string,
    detailsale: Detailsale[]
}
