export interface ProductPLP {
    id: string;
    title: string;
    price: number;
    images: string[];
}
export interface ProductPDP extends ProductPLP {

    description: string;
    attributes: ProductAttribute[];
}

export interface ProductAttribute {
    id: number;
    name: string;
    value: string;
}
