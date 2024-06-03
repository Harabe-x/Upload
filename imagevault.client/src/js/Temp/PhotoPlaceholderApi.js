

export async function getPhotoList(page,limit)
{
    const photoList = await fetch(`https://picsum.photos/v2/list?page=${page}&limit=${limit}`);

    const photoListJson = await photoList.json();
   
    return photoListJson;
}

