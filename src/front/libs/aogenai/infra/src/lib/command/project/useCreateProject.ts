import { IProjectDto } from '@aogenai/domain';
import { MutationHookOptions, useMutation } from '@apollo/client';
import { CreateProjectMutation, GetProjectsQuery } from './cqrs';

interface Request {
  input: { name: string };
}
interface Response {
  project: IProjectDto;
}

export const useCreateProject = (
  options?: MutationHookOptions<Response, Request>
) =>
  useMutation<Response, Request>(CreateProjectMutation, {
    ...options,
    refetchQueries: [GetProjectsQuery],
  });
