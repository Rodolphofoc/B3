export  interface  TaskManager{
    description: string;
    status: number;
    createdAt: string;
    lastModified: string | null;
    deleted: boolean;
    id: number;
    integrationId: string;
}