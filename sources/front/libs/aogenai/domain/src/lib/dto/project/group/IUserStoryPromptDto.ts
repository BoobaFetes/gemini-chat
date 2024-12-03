export interface IUserStoryPromptDto {
  context: string;
  personas: string;
  tasks: string;
  results: string;
}

export function newUserStoryPromptDto(
  obj?: Partial<IUserStoryPromptDto>
): IUserStoryPromptDto {
  return {
    context: obj?.context || '',
    personas: obj?.personas || '',
    tasks: obj?.tasks || '',
    results: obj?.results || '',
  };
}