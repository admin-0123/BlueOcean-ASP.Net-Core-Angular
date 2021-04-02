export interface Product {
    id: string;
    title: string;
    price: number;
    images: string[];
}
// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ProductPLP extends Product {
}
export interface ProductPDP extends ProductPLP {
    description: string;
    attributes: ProductAttribute[];
}

export interface ProductInCart extends Product {
    quantity: number;
}

export interface ProductAttribute {
    id: number;
    name: string;
    value: string;
}

export interface Category {
    value: string;
    title: string;
}
